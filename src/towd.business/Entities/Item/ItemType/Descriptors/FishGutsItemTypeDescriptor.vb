Friend Class FishGutsItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.FishGuts, "Fish Guts", True, "The fish’s slimy innards. 
Fertilize with it or bait a trap—waste not, want not.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
