using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Azure.Kinect.Sensor;
using Microsoft.Azure.Kinect.BodyTracking;
// using Microsoft.Kinect;

namespace MeepEngine
{
    public class EntKinect
        : Entity
    {
        public EntKinect(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Enabled = true;
            Visible = true;
            
            // Set sprite parameters
            sprite = Assets.nosprite;
            imageAngle = 0f;
            imageScale = 1f;
            layer = 0f;
        }

        // Kinect sensor
        public static Device kinect;
        public static Tracker bodytracker;

        public static JointId handType = JointId.HandRight;
        public static Vector2 handPos = new Vector2();

        public static int colorWidth = 0;
        public static int colorHeight = 0;


        public static void InitializeKinect()
        {
            // Find and initialize kinect
            // int count = Device.GetInstalledCount();
            kinect = Device.Open();

            // Initialize if connected
            kinect.StartCameras(new DeviceConfiguration()
            {
                CameraFPS = FPS.FPS30,
                ColorFormat = ImageFormat.ColorBGRA32,
                ColorResolution = ColorResolution.R720p,
                DepthMode = DepthMode.NFOV_Unbinned,
                WiredSyncMode = WiredSyncMode.Standalone,
                SynchronizedImagesOnly = true
            });

            colorWidth = kinect.GetCalibration().ColorCameraCalibration.ResolutionWidth;
            colorHeight = kinect.GetCalibration().ColorCameraCalibration.ResolutionHeight;

            // Initialize bodytracking
            bodytracker = Tracker.Create(kinect.GetCalibration(), new TrackerConfiguration()
            {
                ProcessingMode = TrackerProcessingMode.Gpu,
                SensorOrientation = SensorOrientation.Default
            });
        }

        public static async void AsyncUpdateKinect()
        {
            AsyncUpdateFrame();
            AsyncUpdateTracker();
        }

        protected static async void AsyncUpdateFrame()
        {
            using (Capture sensorCapture = await System.Threading.Tasks.Task.Run(() => { return kinect.GetCapture(); }))
            {
                if (sensorCapture != null)
                {
                    var colorFrame = sensorCapture.Color;

                    if (colorFrame != null)
                    {
                        try
                        {
                            bodytracker.EnqueueCapture(sensorCapture, System.TimeSpan.Zero);
                        }
                        catch
                        {

                        }

                        byte[] pixels = new byte[colorFrame.StrideBytes * colorFrame.HeightPixels];
                        colorFrame.Memory.CopyTo(pixels);

                        Color[] color = new Color[colorFrame.HeightPixels * colorFrame.WidthPixels];
                        // Assets.kinectRGBVideo = new Texture2D(Main.graphics.GraphicsDevice, colorFrame.WidthPixels, colorFrame.HeightPixels);

                        int index = 0;
                        for (int y = 0; y < colorFrame.HeightPixels; y++)
                        {
                            for (int x = 0; x < colorFrame.WidthPixels; x++, index += 4)
                            {
                                color[y * colorFrame.WidthPixels + x] = new Color(pixels[index + 2], pixels[index + 1], pixels[index + 0]);
                            }
                        }

                        Assets.kinectRGBVideo.SetData(color);
                    }
                }
            }
        }

        protected static async void AsyncUpdateTracker()
        {
            using (Frame frame = bodytracker.PopResult(System.TimeSpan.Zero, throwOnTimeout: false))
            {
                if (frame != null)
                {
                    try
                    {
                        Skeleton skeleton = frame.GetBodySkeleton(0);
                        var hand = skeleton.GetJoint(JointId.HandRight);
                        System.Numerics.Vector3 handPos3d = hand.Position;

                        Calibration calibration = kinect.GetCalibration();
                        System.Numerics.Vector2 handPos2d = (System.Numerics.Vector2)calibration.TransformTo2D(handPos3d, CalibrationDeviceType.Depth, CalibrationDeviceType.Color);

                        handPos = new Vector2(handPos2d.X * ((float) Main.roomWidth / (float) colorWidth), handPos2d.Y * ((float) Main.roomHeight / (float) colorHeight));
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
    }
}