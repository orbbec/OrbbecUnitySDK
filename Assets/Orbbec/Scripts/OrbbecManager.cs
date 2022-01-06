using Orbbec;

public interface OrbbecManager
{
    void StartStream(StreamType streamType);
    void StopStream(StreamType streamType);
    void StartAllStream();
    void StopAllStream();
    StreamProfile GetStreamProfile(StreamType streamType);
    void SetStreamProfile(StreamType streamType, StreamProfile profile);
    StreamData GetStreamData(StreamType streamType);
}