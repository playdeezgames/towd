Imports towd.data

Friend Class EatCookedFishFiletVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCookedFishFilet, VerbCategoryType.Eat, ItemType.CookedFishFilet)
    End Sub
End Class
