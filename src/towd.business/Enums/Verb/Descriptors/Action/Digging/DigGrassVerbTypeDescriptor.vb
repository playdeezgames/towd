Friend Class DigGrassVerbTypeDescriptor
    Inherits DigVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.DigGrass,
            "Dig(Grubs)",
            data.LocationType.Grass,
            business.ItemType.Grub)
    End Sub
End Class
