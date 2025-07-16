Friend Class DigGrassVerbTypeDescriptor
    Inherits DigVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.DigGrass,
            "Dig(Grubs)",
            business.LocationType.Grass,
            business.ItemType.Grub,
            "Lookit you... digging in the dirt for insect larvae? How did you come to this?")
    End Sub
End Class
