Imports towd.data

Friend Class EatCookedFishFiletVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.EatCookedFishFilet, business.VerbCategoryType.Eat, ItemType.CookedFishFilet)
    End Sub
End Class
