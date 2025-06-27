Friend Class DigGrassRecipeTypeDescriptor
    Inherits DigRecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.DigGrass,
            "Dig(Grubs)",
            data.LocationType.Grass,
            data.ItemType.Grub)
    End Sub
End Class
