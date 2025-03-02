Friend Class FishHeadItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.FishHead, "Fish Head")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
