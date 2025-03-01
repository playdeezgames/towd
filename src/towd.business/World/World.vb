Imports towd.data

Public Class World
    Inherits WorldDataClient
    Implements IWorld

    Sub New(worldData As WorldData)
        MyBase.New(worldData)
    End Sub

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            If worldData.AvatarId.HasValue Then
                Return New Character(worldData, worldData.AvatarId.Value)
            Else
                Return Nothing
            End If
        End Get
        Set(value As ICharacter)
            If value IsNot Nothing Then
                worldData.AvatarId = value.Id
            Else
                worldData.AvatarId = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property Characters As IEnumerable(Of ICharacter) Implements IWorld.Characters
        Get
            Return Enumerable.Range(0, WorldData.Characters.Count).Select(Function(x) New Character(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property Maps As IEnumerable(Of IMap) Implements IWorld.Maps
        Get
            Return Enumerable.Range(0, WorldData.Maps.Count).Select(Function(x) New Map(WorldData, x))
        End Get
    End Property

    Public Sub Initialize() Implements IWorld.Initialize
        Const MapColumns = 9
        Const MapRows = 9
        Dim map = CreateMap(MapType.Normal.ToDescriptor, MapColumns, MapRows)
        Avatar = CreateCharacter(CharacterType.N00b.ToDescriptor, map.GetLocation(MapColumns \ 2, MapRows \ 2))
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
        With worldData
            .AvatarId = Nothing
            .Characters.Clear()
        End With
    End Sub

    Public Sub AdvanceTime(amount As Integer) Implements IWorld.AdvanceTime
        For Each character In Characters
            character.AdvanceTime(amount)
        Next
        For Each map In Maps
            map.AdvanceTime(amount)
        Next
    End Sub

    Public Function CreateCharacter(characterType As ICharacterType, location As ILocation) As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = WorldData.Characters.Count
        WorldData.Characters.Add(
            New CharacterData With
            {
                .CharacterType = characterType.CharacterType,
                .LocationId = location.Id
            })
        Return New Character(WorldData, characterId)
    End Function

    Public Function CreateLocation(locationType As ILocationType, map As IMap, column As Integer, row As Integer) As ILocation Implements IWorld.CreateLocation
        Dim locationId = WorldData.Locations.Count
        WorldData.Locations.Add(
            New LocationData With
            {
                .LocationType = locationType.LocationType,
                .MapId = map.Id,
                .Column = column,
                .Row = row
            })
        Dim location = New Location(WorldData, locationId)
        locationType.Initialize(location)
        Return location
    End Function

    Public Function CreateMap(mapType As IMapType, columns As Integer, rows As Integer) As IMap Implements IWorld.CreateMap
        Dim mapId = WorldData.Maps.Count
        WorldData.Maps.Add(
            New MapData With
            {
                .MapType = mapType.MapType,
                .Columns = columns,
                .Rows = rows
            })
        Dim map = New Map(WorldData, mapId)
        WorldData.Maps(mapId).Locations = Enumerable.Range(0, columns * rows).
                    Select(Function(x) CreateLocation(
                        mapType.LocationType,
                        map,
                        x Mod columns,
                        x \ columns).Id).ToList
        mapType.Initialize(map)
        Return map
    End Function
End Class
