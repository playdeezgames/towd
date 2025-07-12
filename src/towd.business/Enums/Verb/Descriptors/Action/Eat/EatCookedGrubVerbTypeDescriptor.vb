Imports towd.data

Friend Class EatCookedGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.EatCookedGrub, business.VerbCategoryType.Eat, ItemType.CookedGrub)
    End Sub
End Class
