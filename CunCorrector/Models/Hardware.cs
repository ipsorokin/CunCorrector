using System.ComponentModel;

namespace CunCorrector.Models
{
    internal class Hardware: INotifyPropertyChanged
    {
        public Hardware() { }

        public Hardware(string id, string name, string classId) : this()
        {
            Id = id;
            Name = name;
            ClassId = classId;
        }

        public string Id { get; }
        public string Name { get; }
        public string ClassId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
