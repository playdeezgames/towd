Imports towd.data

Friend Class CookedGrubItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.ItemType.CookedGrub,
            "Cooked Grub",
            True,
            "A roasted grub delicacy. 
Savor it to restore strength—better than raw, if you don’t burn it.",
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
