Friend Class ForageRockVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.ForageRock,
            "Forage(Rocks)",
            business.LocationType.Rock,
            business.ItemType.Rock,
            "You know I love you, but you have a helluvalot to learn about rock and roll.")
    End Sub
End Class
