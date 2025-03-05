Friend Class EatFishVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatFish, "Eat Fish", 0, data.ItemType.CookedFishFilet.ToDescriptor)
    End Sub
End Class
