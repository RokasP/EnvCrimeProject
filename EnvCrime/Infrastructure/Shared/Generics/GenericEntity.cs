namespace EnvCrime.Infrastructure.Shared.Generics
{
    /*
     * Den här klassen behövs för att säkerställa att varje entitetstyp som man skapar i applikationen har en metod att kolla på id hos entiteten.
     * Generiska repositoryt kallar på den här metoden i sin Save(T entity) metod för att lista ut om det är ett nytt objekt eller ett befintligt objekt
     * som man vill spara ner. Man kör ju lite olika sparande logik för nya objekt och existerande objekt.
     */
    public abstract class GenericEntity<U>
    {
        public abstract U GetId();
    }
}
