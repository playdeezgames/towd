Friend Class CookedGrubItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.CookedGrub, "Cooked Grub", True)
    End Sub

    Public Overrides Sub Initialize(item As IItem)
        item.SetStatistic(data.StatisticType.Satiety, 5)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
