Friend Class ForageRockVerbTypeDescriptor
    Inherits ForageVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.ForageRock,
            "Forage(Rocks)",
            data.LocationType.Rock,
            data.ItemType.Rock)
    End Sub
End Class
