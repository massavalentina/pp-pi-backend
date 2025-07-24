namespace PP_PI_Backend.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AutorId {  get; set; }
        
        //public Autor Autor {get; set; }
        //public Editorial Editorial {get; set;}

        //public ICollection<Reseña> Reseñas {get; set;} = new List<Reseña>();
    }




}
