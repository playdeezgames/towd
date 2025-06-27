Friend Class ForagePineRecipeTypeDescriptor
    Inherits ForageRecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            VerbType.ForagePine,
            "Forage(Sticks)",
            data.LocationType.Pine,
            data.ItemType.Stick)
    End Sub
End Class
