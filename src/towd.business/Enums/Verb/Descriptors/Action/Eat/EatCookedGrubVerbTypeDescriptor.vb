Friend Class EatCookedGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatCookedGrub,
            business.VerbCategoryType.Eat,
            ItemType.CookedGrub,
            "It has come to this. Eating bugs. I hope yer proud of yerself.",
            New Dictionary(Of Integer, Integer) From {{0, 1}})
    End Sub
End Class
