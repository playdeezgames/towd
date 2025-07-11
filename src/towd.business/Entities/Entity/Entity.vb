Imports towd.data

Friend MustInherit Class Entity(Of TEntityType, TEntityData As EntityData)
    Inherits WorldDataClient
    Implements IEntity(Of TEntityType)
    Public Sub New(worldData As data.WorldData, entityId As Integer)
        MyBase.New(worldData)
        Me.Id = entityId
    End Sub
    Protected MustOverride ReadOnly Property EntityData As TEntityData
    Public ReadOnly Property Id As Integer Implements IEntity(Of TEntityType).Id
    Public ReadOnly Property World As IWorld Implements IEntity(Of TEntityType).World
        Get
            Return New World(WorldData)
        End Get
    End Property
    Public MustOverride Property EntityType As TEntityType Implements IEntity(Of TEntityType).EntityType

    Public MustOverride Sub AdvanceTime(amount As Integer) Implements IEntity(Of TEntityType).AdvanceTime

    Public Sub SetStatistic(statisticType As String, value As Integer) Implements IEntity(Of TEntityType).SetStatistic
        EntityData.Statistics(statisticType) =
            Math.Clamp(value,
                       GetStatisticMinimum(statisticType),
                       GetStatisticMaximum(statisticType))
    End Sub

    Public Sub SetStatisticMinimum(statisticType As String, value As Integer) Implements IEntity(Of TEntityType).SetStatisticMinimum
        EntityData.StatisticMinimums(statisticType) = value
    End Sub

    Public Sub SetStatisticMaximum(statisticType As String, value As Integer) Implements IEntity(Of TEntityType).SetStatisticMaximum
        EntityData.StatisticMaximums(statisticType) = value
    End Sub

    Public Function GetStatisticMinimum(statisticType As String) As Integer Implements IEntity(Of TEntityType).GetStatisticMinimum
        Dim value As Integer
        If EntityData.StatisticMinimums.TryGetValue(statisticType, value) Then
            Return value
        End If
        Return Integer.MinValue
    End Function

    Public Function GetStatisticMaximum(statisticType As String) As Integer Implements IEntity(Of TEntityType).GetStatisticMaximum
        Dim value As Integer
        If EntityData.StatisticMaximums.TryGetValue(statisticType, value) Then
            Return value
        End If
        Return Integer.MaxValue
    End Function

    Public Function GetStatistic(statisticType As String) As Integer Implements IEntity(Of TEntityType).GetStatistic
        Dim value As Integer = 0
        EntityData.Statistics.TryGetValue(statisticType, value)
        Return Math.Clamp(
            value,
            GetStatisticMinimum(statisticType),
            GetStatisticMaximum(statisticType))
    End Function

    Public Sub ChangeStatistic(statisticType As String, delta As Integer) Implements IEntity(Of TEntityType).ChangeStatistic
        SetStatistic(statisticType, GetStatistic(statisticType) + delta)
    End Sub

    Public Function HasStatistic(statisticType As String) As Boolean Implements IEntity(Of TEntityType).HasStatistic
        Return EntityData.Statistics.ContainsKey(statisticType)
    End Function

    Public Sub ClearStatistic(statisticType As String) Implements IEntity(Of TEntityType).ClearStatistic
        EntityData.Statistics.Remove(statisticType)
    End Sub

    Public Sub SetTag(tagType As String, value As Boolean) Implements IEntity(Of TEntityType).SetTag
        If value Then
            EntityData.Tags.Add(tagType)
        Else
            EntityData.Tags.Remove(tagType)
        End If
    End Sub

    Public Function HasTag(tagType As String) As Boolean Implements IEntity(Of TEntityType).HasTag
        Return EntityData.Tags.Contains(tagType)
    End Function
End Class
