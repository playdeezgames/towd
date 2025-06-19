Friend Class CookedFishFiletItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.CookedFishFilet, "Cooked Fish Filet", True, "A grilled fish slice. 
Eat it to restore vigor—tastes of triumph, if you cooked it right.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
        item.SetStatistic(data.StatisticType.Satiety, 10)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
