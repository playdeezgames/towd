Imports towd.data

Public Class DigVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(
                  verbType As String,
                  name As String,
                  requiredLocationType As String,
                  outputItemType As String,
                  flavorText As String)
        MyBase.New(verbType, business.VerbCategoryType.Dig, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetLocationStatisticMinimum(business.StatisticType.Digging, 1)
        SetLocationStatisticDelta(business.StatisticType.Digging, -1)
        SetCharacterStatisticDelta(business.StatisticType.Digging, 1)
        SetItemTypeOutputGenerator(outputItemType, New DigRangeGenerator)
        SetFlavorText(flavorText)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
