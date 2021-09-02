using System; 
namespace SpiralLab.Sirius 
{
    public class ScriptDemo
    {
        public string Arg1 {get;set;}
        
        public string CustomFormat1
        {   
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd");     
            }
        }       
        public string CustomFormat2
        {   
            get
            {
                return DateTime.Now.ToString("hh:mm:ss");       
            }
        }       
        public string CustomFormat3
        {   
            get
            {
                return DateTime.Now.ToString("yyyyMMdd");       
            }
        }
        public string CustomFormat4
        {   
            get
            {
                return DateTime.Now.ToString("HHmmss");     
            }
        }

    }
}
            