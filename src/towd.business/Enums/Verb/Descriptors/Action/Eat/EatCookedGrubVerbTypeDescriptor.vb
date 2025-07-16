Friend Class EatCookedGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatCookedGrub,
            business.VerbCategoryType.Eat,
            ItemType.CookedGrub,
            "It has come to this. Eating bugs. I hope yer proud of yerself.")
    End Sub
End Class
