Friend Class RawFishItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.RawFish, "Raw Fish")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
