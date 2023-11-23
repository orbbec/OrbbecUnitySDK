using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class Pointcloud : MonoBehaviour
{
    public OrbbecPipeline pipeline;
    public Button pointcloudButton;
    public Button colorPointcloudButton;
    public Text tipsText;

    private PointCloudFilter filter;
    private Format format;
    private bool save;
    private byte[] data;

    private string pointcloudPath;
    private string colorPointcloudPath;
    private bool pointCloudSaved;

    void Start()
    {
        pointcloudPath = Application.persistentDataPath + "/points.ply";
        colorPointcloudPath = Application.persistentDataPath + "/color_points.ply";

        pointcloudButton.onClick.AddListener(SavePointcloud);
        colorPointcloudButton.onClick.AddListener(SaveColorPointcloud);

        pipeline.SetFramesetCallback(OnFrameset);
        pipeline.onPipelineInit.AddListener(()=>{
            pipeline.Config.SetAlignMode(AlignMode.ALIGN_D2C_SW_MODE);
            pipeline.StartPipeline();
            filter = new PointCloudFilter();
            filter.SetCameraParam(pipeline.Pipeline.GetCameraParam());
        });
    }

    void Update()
    {
        if(pointCloudSaved)
        {
            if(format == Format.OB_FORMAT_POINT)
            {
                tipsText.text = "Point cloud saved to: " + pointcloudPath;
            }
            else if(format == Format.OB_FORMAT_RGB_POINT)
            {
                tipsText.text = "Point cloud saved to: " + colorPointcloudPath;
            }
        }
        else
        {
            tipsText.text = "";
        }
    }

    private void OnFrameset(Frameset frameset)
    {
        if(frameset == null)
        {
            return;
        }
        if(save)
        {
            DepthFrame depthFrame = frameset.GetDepthFrame();
            ColorFrame colorFrame = frameset.GetColorFrame();
            if(format == Format.OB_FORMAT_POINT)
            {
                if(depthFrame == null)
                {
                    Debug.LogWarning("Depth frame empty");
                    return;
                }
            }
            if(format == Format.OB_FORMAT_RGB_POINT)
            {
                if(depthFrame == null || colorFrame == null)
                {
                    Debug.LogWarning("Depth or color frame empty");
                    return;
                }
            }
            filter.SetPositionDataScaled(depthFrame.GetValueScale());
            var frame = filter.Process(frameset);
            if(frame != null)
            {
                var pointFrame = frame.As<PointsFrame>();
                var dataSize = pointFrame.GetDataSize();
                if(data == null || data.Length != dataSize)
                {
                    data = new byte[dataSize];
                }
                pointFrame.CopyData(ref data);
                pointFrame.Dispose();
                frame.Dispose();
            }

            pointCloudSaved = false;
            if(format == Format.OB_FORMAT_POINT)
            {
                WritePointPly();
            }
            else if(format == Format.OB_FORMAT_RGB_POINT)
            {
                WriteColorPointPly();
            }
            pointCloudSaved = true;
            save = false;
        }
        frameset.Dispose();
    }

    private void SavePointcloud()
    {
        format = Format.OB_FORMAT_POINT;
        filter.SetPointFormat(format);
        save = true;
    }

    private void SaveColorPointcloud()
    {
        format = Format.OB_FORMAT_RGB_POINT;
        filter.SetPointFormat(format);
        save = true;
    }

    private void WritePointPly()
    {
        int pointSize = Marshal.SizeOf(typeof(Point));
        int pointsSize = data.Length / Marshal.SizeOf(typeof(Point));

        Point[] points = new Point[pointsSize];
        
        IntPtr dataPtr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, dataPtr, data.Length);
        for(int i = 0; i < pointsSize; i++)
        {
            IntPtr pointPtr = new IntPtr(dataPtr.ToInt64() + i * pointSize);
            points[i] = Marshal.PtrToStructure<Point>(pointPtr);
        }
        Marshal.FreeHGlobal(dataPtr);

        FileStream fs = new FileStream(pointcloudPath, FileMode.Create);
        var writer = new StreamWriter(fs);
        writer.Write("ply\n");
        writer.Write("format ascii 1.0\n");
        writer.Write("element vertex " + pointsSize + "\n");
        writer.Write("property float x\n");
        writer.Write("property float y\n");
        writer.Write("property float z\n");
        writer.Write("end_header\n");

        for (int i = 0; i < points.Length; i ++) 
        {
            writer.Write(points[i].x);
            writer.Write(" ");
            writer.Write(points[i].y);
            writer.Write(" ");
            writer.Write(points[i].z);
            writer.Write("\n");
        }
        
        writer.Close();
        fs.Close();
    }

    private void WriteColorPointPly()
    {
        int pointSize = Marshal.SizeOf(typeof(ColorPoint));
        int pointsSize = data.Length / Marshal.SizeOf(typeof(ColorPoint));

        ColorPoint[] points = new ColorPoint[pointsSize];
        
        IntPtr dataPtr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, dataPtr, data.Length);
        for(int i = 0; i < pointsSize; i++)
        {
            IntPtr pointPtr = new IntPtr(dataPtr.ToInt64() + i * pointSize);
            points[i] = Marshal.PtrToStructure<ColorPoint>(pointPtr);
        }
        Marshal.FreeHGlobal(dataPtr);

        FileStream fs = new FileStream(colorPointcloudPath, FileMode.Create);
        var writer = new StreamWriter(fs);
        writer.Write("ply\n");
        writer.Write("format ascii 1.0\n");
        writer.Write("element vertex " + pointsSize + "\n");
        writer.Write("property float x\n");
        writer.Write("property float y\n");
        writer.Write("property float z\n");
        writer.Write("property uchar red\n");
        writer.Write("property uchar green\n");
        writer.Write("property uchar blue\n");
        writer.Write("end_header\n");

        for (int i = 0; i < points.Length; i ++) 
        {
            writer.Write(points[i].x);
            writer.Write(" ");
            writer.Write(points[i].y);
            writer.Write(" ");
            writer.Write(points[i].z);
            writer.Write(" ");
            writer.Write(points[i].r);
            writer.Write(" ");
            writer.Write(points[i].g);
            writer.Write(" ");
            writer.Write(points[i].b);
            writer.Write("\n");
        }
        
        writer.Close();
        fs.Close();
    }
}
