namespace Common
{
    public interface IPixelWriter<T>
    {
        void Put(int x, int y, T color, CyRect? clipRect = null);
        void Put(CyPoint pt, T color, CyRect? clipRect = null);
        void HLine(int x, int y, int w, T color, CyRect? clipRect = null);
        void HLine(CyPoint pt, int w, T color, CyRect? clipRect = null);
        void VLine(int x, int y, int h, T color, CyRect? clipRect = null);
        void VLine(CyPoint pt, int h, T color, CyRect? clipRect = null);
        void Box(int x, int y, int w, int h, T color, CyRect? clipRect = null);
        void Box(CyRect rect, T color, CyRect? clipRect = null);
        void Clear(T color, CyRect? clipRect = null);
    }
}
