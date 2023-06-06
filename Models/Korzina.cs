namespace Kursovaya_BD.Models
{
    public class Korzina
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Названия полей dev и devId должны быть согласованы
        public int modId { get; set; }
        public Model Mod { get; set; } // Ссылка на запись таблицы Model  


        // Названия полей firm и firmId должны быть согласованы
        public int markId { get; set; }
        public Marka Mark { get; set; } // Ссылка на запись таблицы Marka

        public int Price { get; set; }
        public int Kol { get; set; }
    }
}
