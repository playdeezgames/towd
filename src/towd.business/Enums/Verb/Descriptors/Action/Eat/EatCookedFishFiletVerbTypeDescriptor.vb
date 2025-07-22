Imports towd.data

Friend Class EatCookedFishFiletVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatCookedFishFilet,
            business.VerbCategoryType.Eat,
            ItemType.CookedFishFilet,
            "Its got plenty of Omega-3s! Whatever those are!",
            New Dictionary(Of Integer, Integer) From {{0, 1}})
    End Sub
End Class
