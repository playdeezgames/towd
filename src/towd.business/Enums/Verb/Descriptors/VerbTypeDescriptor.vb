Imports System.Text
Imports towd.data

Public MustInherit Class VerbTypeDescriptor
    Implements IVerbType
    Private ReadOnly locationStatisticMinimums As New Dictionary(Of String, Integer)
    Private ReadOnly locationStatisticDeltas As New Dictionary(Of String, Integer)
    Private ReadOnly characterStatisticDeltas As New Dictionary(Of String, Integer)
    Private ReadOnly itemTypeInputs As New Dictionary(Of String, Integer)
    Private ReadOnly itemTypeInputDurabilities As New Dictionary(Of String, Integer)
    Private ReadOnly characterStatisticMinimums As New Dictionary(Of String, Integer)
    Private ReadOnly characterStatisticMaximums As New Dictionary(Of String, Integer)
    Private ReadOnly requiredLocationTypes As New HashSet(Of data.LocationType)
    Private ReadOnly itemTypeOutputGenerators As New Dictionary(Of String, ICharacterWeightedGenerator)
    Private buildsLocationType As data.LocationType? = Nothing
    Private displayName As String = Nothing
    Private ReadOnly timeTaken As Integer
    Protected Sub SetDisplayName(displayName As String)
        Me.displayName = displayName
    End Sub
    Protected Sub SetLocationStatisticMinimum(statisticType As String, minimum As Integer)
        locationStatisticMinimums(statisticType) = minimum
    End Sub
    Protected Sub SetLocationStatisticDelta(statisticType As String, delta As Integer)
        locationStatisticDeltas(statisticType) = delta
    End Sub
    Protected Sub SetCharacterStatisticDelta(statisticType As String, delta As Integer)
        characterStatisticDeltas(statisticType) = delta
    End Sub
    Protected Sub SetBuildsLocationType(locationType As data.LocationType?)
        Me.buildsLocationType = locationType
    End Sub
    Protected Sub SetRequiredLocationType(locationType As data.LocationType)
        requiredLocationTypes.Add(locationType)
    End Sub
    Protected Sub SetCharacterStatisticMinimum(statisticType As String, minimum As Integer)
        characterStatisticMinimums(statisticType) = minimum
    End Sub
    Protected Sub SetCharacterStatisticMaximum(statisticType As String, maximum As Integer)
        characterStatisticMaximums(statisticType) = maximum
    End Sub
    Protected Sub SetItemTypeInput(itemType As String, quantity As Integer)
        itemTypeInputs(itemType) = quantity
    End Sub
    Protected Sub SetItemTypeInputDurability(itemType As String, quantity As Integer)
        itemTypeInputDurabilities(itemType) = quantity
    End Sub
    Protected Sub SetItemTypeOutputGenerator(itemType As String, generator As ICharacterWeightedGenerator)
        itemTypeOutputGenerators(itemType) = generator
    End Sub
    Sub New(verbType As VerbType, verbCategoryType As VerbCategoryType, timeTaken As Integer)
        Me.VerbType = verbType
        Me.timeTaken = timeTaken
        Me.VerbCategoryType = verbCategoryType
    End Sub
    Public ReadOnly Property VerbType As VerbType Implements IVerbType.VerbType

    Public ReadOnly Property Name As String Implements IVerbType.Name
        Get
            If Not String.IsNullOrWhiteSpace(displayName) Then
                Return displayName
            End If
            Dim builder As New StringBuilder
            builder.Append(String.Join("+"c, itemTypeInputs.Select(Function(x) $"{x.Value} {x.Key.ToItemTypeDescriptor.Name}")))
            builder.Append("->")
            Dim outputs = itemTypeOutputGenerators.Select(Function(x) $"{x.Value} {x.Key.ToItemTypeDescriptor.Name}").ToList
            If buildsLocationType.HasValue Then
                outputs.Add($"Builds {buildsLocationType.Value.ToLocationTypeDescriptor.Name}")
            End If
            builder.Append(String.Join("+"c, outputs))
            Return builder.ToString()
        End Get
    End Property

    Public ReadOnly Property Description As String Implements IVerbType.Description
        Get
            Dim builder As New StringBuilder
            If itemTypeInputs.Any Then
                builder.AppendLine("Item Type Inputs:")
                For Each entry In itemTypeInputs
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToItemTypeDescriptor.Name}")
                Next
            End If
            If itemTypeInputDurabilities.Any Then
                builder.AppendLine("Item Type Input Durabilities:")
                For Each entry In itemTypeInputDurabilities
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToItemTypeDescriptor.Name}")
                Next
            End If
            If itemTypeOutputGenerators.Any Then
                builder.AppendLine("Item Type Outputs:")
                For Each entry In itemTypeOutputGenerators
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToItemTypeDescriptor.Name}")
                Next
            End If
            If characterStatisticMinimums.Any Then
                builder.AppendLine("Character Statistic Minimums:")
                For Each entry In characterStatisticMinimums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToStatisticTypeDescriptor.Name}")
                Next
            End If
            If characterStatisticMaximums.Any Then
                builder.AppendLine("Character Statistic Maximums:")
                For Each entry In characterStatisticMaximums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToStatisticTypeDescriptor.Name}")
                Next
            End If
            If requiredLocationTypes.Any Then
                builder.AppendLine("Required Location Type:")
                For Each entry In requiredLocationTypes
                    builder.AppendLine($"  {entry.ToLocationTypeDescriptor.Name}")
                Next
            End If
            If locationStatisticMinimums.Any Then
                builder.AppendLine("Location Statistic Minimums:")
                For Each entry In locationStatisticMinimums
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToStatisticTypeDescriptor.Name}")
                Next
            End If
            If locationStatisticDeltas.Any Then
                builder.AppendLine("Location Statistic Deltas:")
                For Each entry In locationStatisticDeltas
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToStatisticTypeDescriptor.Name}")
                Next
            End If
            If characterStatisticDeltas.Any Then
                builder.AppendLine("Character Statistic Deltas:")
                For Each entry In characterStatisticDeltas
                    builder.AppendLine($"  {entry.Value} {entry.Key.ToStatisticTypeDescriptor.Name}")
                Next
            End If
            If buildsLocationType.HasValue Then
                builder.AppendLine($"Builds: {buildsLocationType.Value.ToLocationTypeDescriptor.Name}")
            End If
            Return builder.ToString()
        End Get
    End Property

    Public ReadOnly Property VerbCategoryType As VerbCategoryType Implements IVerbType.VerbCategoryType

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public Sub Perform(character As ICharacter) Implements IVerbType.Perform
        If Not CanPerform(character) Then
            Return
        End If
        For Each entry In locationStatisticDeltas
            character.Location.ChangeStatistic(entry.Key, entry.Value)
        Next
        For Each entry In characterStatisticDeltas
            character.ChangeStatistic(entry.Key, entry.Value)
            character.AppendMessage($"{entry.Value} {entry.Key.ToStatisticTypeDescriptor.Name}({character.GetStatistic(entry.Key)})")
        Next
        Dim quantities As New Dictionary(Of String, Integer)
        For Each entry In itemTypeOutputGenerators
            quantities(entry.Key) = entry.Value.Generate(character)
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
                    character.RemoveItemOfType(entry.Key.ToItemTypeDescriptor)
                Next
                character.AppendMessage($"{entry.Value} {entry.Key.ToItemTypeDescriptor.Name}(x{character.GetCountOfItemType(entry.Key.ToItemTypeDescriptor)})")
            ElseIf entry.Value > 0 Then
                For Each dummy In Enumerable.Range(0, entry.Value)
                    character.AddItem(character.World.CreateItem(entry.Key.ToItemTypeDescriptor))
                Next
                character.AppendMessage($"+{entry.Value} {entry.Key.ToItemTypeDescriptor.Name}(x{character.GetCountOfItemType(entry.Key.ToItemTypeDescriptor)})")
            End If
        Next
        For Each entry In itemTypeInputDurabilities
            For Each dummy In Enumerable.Range(0, entry.Value)
                Dim item = character.GetItemsOfType(entry.Key.ToItemTypeDescriptor).First
                character.ChangeItemDurability(item, -1)
            Next
        Next
        If buildsLocationType.HasValue Then
            character.Location.EntityType = buildsLocationType.Value.ToLocationTypeDescriptor
            character.AppendMessage($"Changed location to {buildsLocationType.Value.ToLocationTypeDescriptor.Name}.")
        End If
        character.World.AdvanceTime(timeTaken)
    End Sub
    Public Function CanPerform(character As ICharacter) As Boolean Implements IVerbType.CanPerform
        Dim location = character.Location
        For Each entry In locationStatisticMinimums
            If location.GetStatistic(entry.Key) < entry.Value Then
                Return False
            End If
        Next
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
            If character.GetCountOfItemType(entry.Key.ToItemTypeDescriptor) < entry.Value Then
                Return False
            End If
        Next
        For Each entry In itemTypeInputDurabilities
            If character.GetStatisticSumOfItemType(entry.Key.ToItemTypeDescriptor, StatisticType.Durability) < entry.Value Then
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
