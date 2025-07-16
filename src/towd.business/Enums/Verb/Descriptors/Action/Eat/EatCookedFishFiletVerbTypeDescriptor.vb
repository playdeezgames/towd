Imports towd.data

Friend Class EatCookedFishFiletVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatCookedFishFilet,
            business.VerbCategoryType.Eat,
            ItemType.CookedFishFilet,
            "Its got plenty of Omega-3s! Whatever those are!")
    End Sub
End Class
