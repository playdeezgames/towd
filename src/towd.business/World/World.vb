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


    Public Sub Initialize() Implements IWorld.Initialize
        Const MapColumns = 9
        Const MapRows = 9
        Dim map = CreateMap(MapType.Normal, MapColumns, MapRows)
        Dim location = CreateLocation(LocationType.Grass)
        Avatar = CreateCharacter(CharacterType.N00b, Location)
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
        With worldData
            .AvatarId = Nothing
            .Characters.Clear()
        End With
    End Sub

    Public Function CreateCharacter(characterType As CharacterType, location As ILocation) As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = worldData.Characters.Count
        worldData.Characters.Add(
            New CharacterData With
            {
                .CharacterType = characterType,
                .LocationId = location.Id
            })
        Return New Character(worldData, characterId)
    End Function

    Public Function CreateLocation(locationType As LocationType) As ILocation Implements IWorld.CreateLocation
        Dim locationId = worldData.Locations.Count
        worldData.Locations.Add(
            New LocationData With
            {
                .LocationType = locationType
            })
        Return New Location(worldData, locationId)
    End Function

    Public Function CreateMap(mapType As MapType, columns As Integer, rows As Integer) As IMap Implements IWorld.CreateMap
        Dim mapId = WorldData.Maps.Count
        WorldData.Maps.Add(
            New MapData With
            {
                .MapType = mapType,
                .Columns = columns,
                .Rows = rows,
                .Locations = Enumerable.Range(0, columns * rows).Select(Function(x) CreateLocation(mapType.ToDescriptor().LocationType).Id).ToList
            })
        Return New Map(WorldData, mapId)
    End Function
End Class
