Friend Class RawFishFiletItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.RawFishFilet, "Raw Fish Filet", True, "A sliced fish portion. 
Cook it for a meal, but don’t let it rot—freshness is fleeting.",
            statistics:=New Dictionary(Of String, Integer) From
            {
                {StatisticType.Satiety, 10}
            })
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
