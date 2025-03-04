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

    Public Sub SetStatistic(statisticType As StatisticType, value As Integer) Implements IEntity(Of TEntityType).SetStatistic
        EntityData.Statistics(statisticType) =
            Math.Clamp(value,
                       GetStatisticMinimum(statisticType),
                       GetStatisticMaximum(statisticType))
    End Sub

    Public Sub SetStatisticMinimum(statisticType As StatisticType, value As Integer) Implements IEntity(Of TEntityType).SetStatisticMinimum
        EntityData.StatisticMinimums(statisticType) = value
    End Sub

    Public Sub SetStatisticMaximum(statisticType As StatisticType, value As Integer) Implements IEntity(Of TEntityType).SetStatisticMaximum
        EntityData.StatisticMaximums(statisticType) = value
    End Sub

    Public Function HasFlag(flagType As FlagType) As Boolean Implements IEntity(Of TEntityType).HasFlag
        Return EntityData.Flags.Contains(flagType)
    End Function

    Public Sub SetFlag(flagType As FlagType, flagValue As Boolean) Implements IEntity(Of TEntityType).SetFlag
        If flagValue Then
            EntityData.Flags.Add(flagType)
        Else
            EntityData.Flags.Remove(flagType)
        End If
    End Sub

    Public Function GetStatisticMinimum(statisticType As StatisticType) As Integer Implements IEntity(Of TEntityType).GetStatisticMinimum
        Dim value As Integer
        If EntityData.StatisticMinimums.TryGetValue(statisticType, value) Then
            Return value
        End If
        Return Integer.MinValue
    End Function

    Public Function GetStatisticMaximum(statisticType As StatisticType) As Integer Implements IEntity(Of TEntityType).GetStatisticMaximum
        Dim value As Integer
        If EntityData.StatisticMaximums.TryGetValue(statisticType, value) Then
            Return value
        End If
        Return Integer.MaxValue
    End Function

    Public Function GetStatistic(statisticType As StatisticType) As Integer Implements IEntity(Of TEntityType).GetStatistic
        Dim value As Integer = 0
        EntityData.Statistics.TryGetValue(statisticType, value)
        Return Math.Clamp(
            value,
            GetStatisticMinimum(statisticType),
            GetStatisticMaximum(statisticType))
    End Function

    Public Sub ChangeStatistic(statisticType As StatisticType, delta As Integer) Implements IEntity(Of TEntityType).ChangeStatistic
        SetStatistic(statisticType, GetStatistic(statisticType) + delta)
    End Sub

    Public Function HasStatistic(statisticType As StatisticType) As Boolean Implements IEntity(Of TEntityType).HasStatistic
        Return EntityData.Statistics.ContainsKey(statisticType)
    End Function

    Public Sub ClearStatistic(statisticType As StatisticType) Implements IEntity(Of TEntityType).ClearStatistic
        EntityData.Statistics.Remove(statisticType)
    End Sub
End Class
