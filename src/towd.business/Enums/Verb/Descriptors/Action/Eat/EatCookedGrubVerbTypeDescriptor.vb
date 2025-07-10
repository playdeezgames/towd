Imports towd.data

Friend Class EatCookedGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCookedGrub, VerbCategoryType.Eat, ItemType.CookedGrub)
    End Sub
End Class
