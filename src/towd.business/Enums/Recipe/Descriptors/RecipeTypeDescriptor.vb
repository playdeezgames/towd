Imports System.Text
Imports towd.data

Public MustInherit Class RecipeTypeDescriptor
    Implements IRecipeType
    'location statistic minimum
    'location statistic delta
    'character statistic delta
    Private ReadOnly itemTypeInputs As New Dictionary(Of data.ItemType, Integer)
    Private ReadOnly itemTypeInputDurabilities As New Dictionary(Of data.ItemType, Integer)
    Private ReadOnly characterStatisticMinimums As New Dictionary(Of data.StatisticType, Integer)
    Private ReadOnly characterStatisticMaximums As New Dictionary(Of data.StatisticType, Integer)
    Private ReadOnly requiredLocationTypes As New HashSet(Of data.LocationType)
    Private ReadOnly itemTypeOutputs As New Dictionary(Of data.ItemType, Integer)
    'itemTypeOutputGenerators
    Private buildsLocationType As data.LocationType? = Nothing
    Private ReadOnly timeTaken As Integer
    Protected Sub SetBuildsLocationType(locationType As data.LocationType?)
        Me.buildsLocationType = locationType
    End Sub
    Protected Sub SetRequiredLocationType(locationType As data.LocationType)
        requiredLocationTypes.Add(locationType)
    End Sub
    Protected Sub SetCharacterStatisticMinimum(statisticType As StatisticType, minimum As Integer)
        characterStatisticMinimums(statisticType) = minimum
    End Sub
    Protected Sub SetCharacterStatisticMaximum(statisticType As StatisticType, maximum As Integer)
        characterStatisticMaximums(statisticType) = maximum
    End Sub
    Protected Sub SetItemTypeInput(itemType As data.ItemType, quantity As Integer)
        itemTypeInputs(itemType) = quantity
    End Sub
    Protected Sub SetItemTypeInputDurability(itemType As data.ItemType, quantity As Integer)
        itemTypeInputDurabilities(itemType) = quantity
    End Sub
    Protected Sub SetItemTypeOutput(itemType As data.ItemType, quantity As Integer)
        itemTypeOutputs(itemType) = quantity
    End Sub
    Sub New(recipeType As RecipeType, timeTaken As Integer)
        Me.RecipeType = recipeType
        Me.timeTaken = timeTaken
    End Sub
    Public ReadOnly Property RecipeType As RecipeType Implements IRecipeType.RecipeType

    Public ReadOnly Property Name As String Implements IRecipeType.Name
        Get
            Dim builder As New StringBuilder
            builder.Append(String.Join("+"c, itemTypeInputs.Select(Function(x) $"{x.Value} {x.Key.ToDescriptor.Name}")))
            builder.Append("->")
            Dim outputs = itemTypeOutputs.Select(Function(x) $"{x.Value} {x.Key.ToDescriptor.Name}").ToList
            If buildsLocationType.HasValue Then
                outputs.Add($"Builds {buildsLocationType.Value.ToDescriptor.Name}")
            End If
            builder.Append(String.Join("+"c, outputs))
            Return builder.ToString()
        End Get
    End Property

    Public ReadOnly Property Description As String Implements IRecipeType.Description
        Get
            Dim builder As New StringBuilder
            If itemTypeInputs.Any Then
                builder.AppendLine("Item Type Inputs:")
                For Each entry In itemTypeInputs
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If itemTypeInputDurabilities.Any Then
                builder.AppendLine("Item Type Input Durabilities:")
                For Each entry In itemTypeInputDurabilities
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If itemTypeOutputs.Any Then
                builder.AppendLine("Item Type Outputs:")
                For Each entry In itemTypeOutputs
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If characterStatisticMinimums.Any Then
                builder.AppendLine("Character Statistic Minimums:")
                For Each entry In characterStatisticMinimums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If characterStatisticMaximums.Any Then
                builder.AppendLine("Character Statistic Maximums:")
                For Each entry In characterStatisticMaximums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If requiredLocationTypes.Any Then
                builder.AppendLine("Required Location Type:")
                For Each entry In requiredLocationTypes
                    builder.AppendLine($"  {entry.ToDescriptor.Name}")
                Next
            End If
            If buildsLocationType.HasValue Then
                builder.AppendLine($"Builds: {buildsLocationType.Value.ToDescriptor.Name}")
            End If
            Return builder.ToString()
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public Sub Craft(character As ICharacter) Implements IRecipeType.Craft
        If Not CanCraft(character) Then
            Return
        End If
        character.LastRecipe = RecipeType
        Dim quantities As New Dictionary(Of data.ItemType, Integer)
        For Each entry In itemTypeOutputs
            quantities(entry.Key) = entry.Value
        Next
        For Each entry In itemTypeInputs
            If Not quantities.ContainsKey(entry.Key) Then
                quantities(entry.Key) = 0
            End If
            quantities(entry.Key) -= entry.Value
        Next
        For Each entry In quantities
            If entry.Value < 0 Then
                For Each dummy In Enumerable.Range(0, -entry.Value)
                    character.RemoveItemOfType(entry.Key.ToDescriptor)
                Next
                character.AppendMessage($"{entry.Value} {entry.Key.ToDescriptor.Name}(x{character.GetCountOfItemType(entry.Key.ToDescriptor)})")
            ElseIf entry.Value > 0 Then
                For Each dummy In Enumerable.Range(0, entry.Value)
                    character.AddItem(character.World.CreateItem(entry.Key.ToDescriptor))
                Next
                character.AppendMessage($"+{entry.Value} {entry.Key.ToDescriptor.Name}(x{character.GetCountOfItemType(entry.Key.ToDescriptor)})")
            End If
        Next
        For Each entry In itemTypeInputDurabilities
            For Each dummy In Enumerable.Range(0, entry.Value)
                Dim item = character.GetItemsOfType(entry.Key.ToDescriptor).First
                character.ChangeItemDurability(item, -1)
            Next
        Next
        character.ChangeStatistic(StatisticType.CraftCounter, 1)
        If buildsLocationType.HasValue Then
            character.Location.EntityType = buildsLocationType.Value.ToDescriptor
            character.AppendMessage($"Changed location to {buildsLocationType.Value.ToDescriptor.Name}.")
        End If
        character.World.AdvanceTime(timeTaken)
        character.SetFlag(data.FlagType.CraftMenu, VerbType.Craft.ToDescriptor.CanPerform(character))
    End Sub
    Public Function CanCraft(character As ICharacter) As Boolean Implements IRecipeType.CanCraft
        For Each entry In characterStatisticMinimums
            If character.GetStatistic(entry.Key) < entry.Value Then
                Return False
            End If
        Next
        For Each entry In characterStatisticMaximums
            If character.GetStatistic(entry.Key) > entry.Value Then
                Return False
            End If
        Next
        For Each entry In itemTypeInputs
            If character.GetCountOfItemType(entry.Key.ToDescriptor) < entry.Value Then
                Return False
            End If
        Next
        For Each entry In itemTypeInputDurabilities
            If character.GetStatisticSumOfItemType(entry.Key.ToDescriptor, StatisticType.Durability) < entry.Value Then
                Return False
            End If
        Next
        If requiredLocationTypes.Any Then
            If Not requiredLocationTypes.Contains(character.Location.EntityType.LocationType) Then
                Return False
            End If
        End If
        Return True
    End Function
End Class
