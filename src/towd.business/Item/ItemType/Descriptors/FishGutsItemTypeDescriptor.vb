Friend Class FishGutsItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.FishGuts, "Fish Guts")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
