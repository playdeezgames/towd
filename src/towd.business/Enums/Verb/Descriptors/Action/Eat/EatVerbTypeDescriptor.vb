Public Class EatVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly foodPoisoningGenerator As IReadOnlyDictionary(Of Integer, Integer)

    Public Sub New(
                  verbType As String,
                  verbCategoryType As String,
                  itemType As String,
                  flavorText As String,
                  foodPoisoningGenerator As IReadOnlyDictionary(Of Integer, Integer))
        MyBase.New(verbType, verbCategoryType, 0)
        Dim itemTypeDescriptor = itemType.ToItemTypeDescriptor
        SetDisplayName($"Eat {itemTypeDescriptor.Name}")
        SetItemTypeInput(itemType, 1)
        SetCharacterStatisticDelta(StatisticType.Satiety, itemTypeDescriptor.Statistics(StatisticType.Satiety))
        SetFlavorText(flavorText)
        Me.foodPoisoningGenerator = foodPoisoningGenerator
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        Dim foodPoisoning = RNG.Generate(foodPoisoningGenerator)
        If foodPoisoning > 0 Then
            character.ChangeStatistic(StatisticType.FoodPoisoning, foodPoisoning)
            character.AppendMessage($"+{foodPoisoning} {StatisticType.FoodPoisoning.ToSkillTypeDescriptor.Name}")
        End If
    End Sub
End Class
