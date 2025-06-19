Friend Class RawFishItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.RawFish, "Raw Fish", True, "A slippery catch from the deep. 
Gut it or cook it—eat quick, or it spoils in the sun.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
