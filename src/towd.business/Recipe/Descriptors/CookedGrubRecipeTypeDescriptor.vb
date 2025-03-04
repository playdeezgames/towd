Imports towd.data

Friend Class CookedGrubRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookedGrub)
        SetInput(ItemType.Grub, 1)
        SetInput(ItemType.SharpStick, 1)
        SetInputDurability(ItemType.SharpStick, 1)
        SetOutput(ItemType.SharpStick, 1)
        SetOutput(ItemType.CookedGrub, 1)
    End Sub
    Protected Overrides Function Precondition(Character As ICharacter) As Boolean
        Return Character.Location.EntityType.LocationType = LocationType.CookingFire
    End Function
End Class
