namespace Converted_POS.Models
{
    public class KeymapModel
    {
        public int? keymapId { get; set; }
        public string Text { get; set; }  // This is the name or description of the keymap
        public int shortingNum { get; set; }
        public int tillId { get; set; }
        public bool IsChecked { get; set; }
    }


}