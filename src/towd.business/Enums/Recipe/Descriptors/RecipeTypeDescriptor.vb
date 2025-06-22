Imports System.Text
Imports towd.data

Public MustInherit Class RecipeTypeDescriptor
    Implements IRecipeType
    Private ReadOnly inputs As New Dictionary(Of data.ItemType, Integer)
    Private ReadOnly inputDurabilities As New Dictionary(Of data.ItemType, Integer)
    Private ReadOnly statisticMinimums As New Dictionary(Of data.StatisticType, Integer)
    Private ReadOnly statisticMaximums As New Dictionary(Of data.StatisticType, Integer)
    Private ReadOnly requiredLocations As New HashSet(Of data.LocationType)
    Private ReadOnly itemTypeOutputs As New Dictionary(Of data.ItemType, Integer)
    Private locationTypeOutput As data.LocationType? = Nothing
    Private ReadOnly timeTaken As Integer
    Protected Sub SetLocationTypeOutput(locationType As data.LocationType?)
        Me.locationTypeOutput = locationType
    End Sub
    Protected Sub SetRequiredLocation(locationType As data.LocationType)
        requiredLocations.Add(locationType)
    End Sub
    Protected Sub SetStatisticMinimum(statisticType As StatisticType, minimum As Integer)
        statisticMinimums(statisticType) = minimum
    End Sub
    Protected Sub SetStatisticMaximum(statisticType As StatisticType, maximum As Integer)
        statisticMaximums(statisticType) = maximum
    End Sub
    Protected Sub SetInput(itemType As data.ItemType, quantity As Integer)
        inputs(itemType) = quantity
    End Sub
    Protected Sub SetInputDurability(itemType As data.ItemType, quantity As Integer)
        inputDurabilities(itemType) = quantity
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
            builder.Append(String.Join("+"c, inputs.Select(Function(x) $"{x.Value} {x.Key.ToDescriptor.Name}")))
            builder.Append("->")
            Dim outputs = itemTypeOutputs.Select(Function(x) $"{x.Value} {x.Key.ToDescriptor.Name}").ToList
            If locationTypeOutput.HasValue Then
                outputs.Add($"Builds {locationTypeOutput.Value.ToDescriptor.Name}")
            End If
            builder.Append(String.Join("+"c, outputs))
            Return builder.ToString()
        End Get
    End Property

    Public ReadOnly Property Description As String Implements IRecipeType.Description
        Get
            Dim builder As New StringBuilder
            If inputs.Any Then
                builder.AppendLine("Inputs:")
                For Each entry In inputs
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If inputDurabilities.Any Then
                builder.AppendLine("Input Durabilities:")
                For Each entry In inputDurabilities
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If itemTypeOutputs.Any Then
                builder.AppendLine("Outputs:")
                For Each entry In itemTypeOutputs
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If statisticMinimums.Any Then
                builder.AppendLine("Statistic Minimums:")
                For Each entry In statisticMinimums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If statisticMaximums.Any Then
                builder.AppendLine("Statistic Maximums:")
                For Each entry In statisticMaximums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToDescriptor.Name}")
                Next
            End If
            If requiredLocations.Any Then
                builder.AppendLine("Required Location Type:")
                For Each entry In requiredLocations
                    builder.AppendLine($"  {entry.ToDescriptor.Name}")
                Next
            End If
            If locationTypeOutput.HasValue Then
                builder.AppendLine($"Builds: {locationTypeOutput.Value.ToDescriptor.Name}")
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
        For Each entry In inputs
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
        For Each entry In inputDurabilities
            For Each dummy In Enumerable.Range(0, entry.Value)
                Dim item = character.GetItemsOfType(entry.Key.ToDescriptor).First
                character.ChangeItemDurability(item, -1)
            Next
        Next
        character.ChangeStatistic(StatisticType.CraftCounter, 1)
        If locationTypeOutput.HasValue Then
            character.Location.EntityType = locationTypeOutput.Value.ToDescriptor
            character.AppendMessage($"Changed location to {locationTypeOutput.Value.ToDescriptor.Name}.")
        End If
        character.World.AdvanceTime(timeTaken)
        character.SetFlag(data.FlagType.CraftMenu, VerbType.Craft.ToDescriptor.CanPerform(character))
    End Sub
    Public Function CanCraft(character As ICharacter) As Boolean Implements IRecipeType.CanCraft
        For Each entry In statisticMinimums
            If character.GetStatistic(entry.Key) < entry.Value Then
                Return False
            End If
        Next
        For Each entry In statisticMaximums
            If character.GetStatistic(entry.Key) > entry.Value Then
                Return False
            End If
        Next
        For Each entry In inputs
            If character.GetCountOfItemType(entry.Key.ToDescriptor) < entry.Value Then
                Return False
            End If
        Next
        For Each entry In inputDurabilities
            If character.GetStatisticSumOfItemType(entry.Key.ToDescriptor, StatisticType.Durability) < entry.Value Then
                Return False
            End If
        Next
        If requiredLocations.Any Then
            If Not requiredLocations.Contains(character.Location.EntityType.LocationType) Then
                Return False
            End If
        End If
        Return True
    End Function
End Class
