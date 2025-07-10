Friend Class ForageRockVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.ForageRock,
            "Forage(Rocks)",
            business.LocationType.Rock,
            business.ItemType.Rock)
    End Sub
End Class
