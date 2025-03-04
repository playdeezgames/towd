Imports towd.data

Friend Class CookedFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookedFishFilet, 1)
        SetInput(ItemType.RawFishFilet, 1)
        SetInput(ItemType.SharpStick, 1)
        SetInputDurability(ItemType.SharpStick, 1)
        SetOutput(ItemType.SharpStick, 1)
        SetOutput(ItemType.CookedFishFilet, 1)
    End Sub
    Protected Overrides Function Precondition(Character As ICharacter) As Boolean
        Return Character.Location.EntityType.LocationType = LocationType.CookingFire
    End Function
End Class
