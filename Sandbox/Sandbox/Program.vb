Imports System.IO
Imports System.Text.Json
Imports TiledLib
Imports TiledLib.Layer
Imports TiledLib.Objects
Module Program
    Sub Main(args As String())
        ProcessFile("E:\GIT\towd\Maps\Home.tmx", "Home.json")
        ProcessFile("E:\GIT\towd\Maps\Left.tmx", "Loft.json")
        ProcessFile("E:\GIT\towd\Maps\Quotidian.tmx", "Quotidian.json")
    End Sub

    Private Sub ProcessFile(inputFilename As String, outputFilename As String)
        Dim data As New MapData
        Using stream = File.OpenRead(inputFilename)
            Dim tileTable As New Dictionary(Of Integer, (ITileset, Integer))
            Dim fromMap = Map.FromStream(stream, Function(ts) File.OpenRead(Path.Combine(Path.GetDirectoryName(inputFilename), ts.Source)))
            data.Columns = fromMap.Width
            data.Rows = fromMap.Height
            data.Cells.Clear()
            For Each tileSet In fromMap.Tilesets
                For tileIndex = 0 To tileSet.TileCount - 1
                    tileTable(tileSet.FirstGid + tileIndex) = (tileSet, tileIndex)
                Next
            Next
            For Each l In fromMap.Layers
                Select Case l.LayerType
                    Case LayerType.tilelayer
                        ProcessTileLayer(DirectCast(l, TileLayer), tileTable, data)
                    Case LayerType.objectgroup
                        ProcessObjectGroup(DirectCast(l, ObjectLayer), tileTable, fromMap.CellWidth, fromMap.CellHeight, data)
                    Case Else
                        Throw New NotImplementedException
                End Select
            Next
        End Using
        File.WriteAllText(outputFilename, JsonSerializer.Serialize(data))
    End Sub

    Private Sub ProcessObjectGroup(l As ObjectLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), cellWidth As Integer, cellHeight As Integer, data As MapData)
        Dim objectTable = l.Objects.ToDictionary(Function(x) x.Id, Function(x) DirectCast(x, TileObject))
        Dim eventTable = l.Objects.ToDictionary(Function(x) x.Id, Function(x) New EventData)
        For Each entry In objectTable
            Dim eventId = entry.Key
            Dim obj = entry.Value
            Dim tile = tileTable(obj.Gid)
            Dim properties = tile.Item1.TileProperties(tile.Item2).ToDictionary(Function(x) x.Key, Function(x) x.Value)
            For Each prop In obj.Properties
                properties(prop.Key) = prop.Value
            Next
            Select Case properties("EventType")
                Case "Message"
                    eventTable(eventId).EventType = EventType.Message
                    eventTable(eventId).Strings(EventString.Message) = properties("Message")
                    If properties("NextEvent") <> "0" Then
                        eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
                    End If
                Case "Teleport"
                    eventTable(eventId).EventType = EventType.Teleport
                    eventTable(eventId).Integers(EventInteger.ToX) = CInt(properties("ToX"))
                    eventTable(eventId).Integers(EventInteger.ToY) = CInt(properties("ToY"))
                    eventTable(eventId).Strings(EventString.ToMap) = properties("ToMap")
                    If properties("NextEvent") <> "0" Then
                        eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
                    End If
                Case "CheckFlag"
                    eventTable(eventId).EventType = EventType.CheckFlag
                    eventTable(eventId).Strings(EventString.Flag) = properties("Flag")
                    If properties("WhenClear") <> "0" Then
                        eventTable(eventId).Links(LinkType.WhenClear) = eventTable(CInt(properties("WhenClear")))
                    End If
                    If properties("WhenSet") <> "0" Then
                        eventTable(eventId).Links(LinkType.WhenSet) = eventTable(CInt(properties("WhenSet")))
                    End If
                Case "SetFlag"
                    eventTable(eventId).EventType = EventType.SetFlag
                    eventTable(eventId).Strings(EventString.Flag) = properties("Flag")
                    If properties("NextEvent") <> "0" Then
                        eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
                    End If
                Case "GiveItem"
                    eventTable(eventId).EventType = EventType.GiveItem
                    eventTable(eventId).Integers(EventInteger.ItemId) = CInt(properties("ItemId"))
                    eventTable(eventId).Integers(EventInteger.ItemCount) = CInt(properties("ItemCount"))
                    If properties("NextEvent") <> "0" Then
                        eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
                    End If
                Case "Trigger"
                    eventTable(eventId).EventType = EventType.Trigger
                    If properties("OnBump") <> "0" Then
                        eventTable(eventId).Links(LinkType.OnBump) = eventTable(CInt(properties("OnBump")))
                    End If
                    If properties("OnEnter") <> "0" Then
                        eventTable(eventId).Links(LinkType.OnEnter) = eventTable(CInt(properties("OnEnter")))
                    End If
                    Dim column = CInt(obj.X) \ cellWidth
                    Dim row = CInt(obj.Y) \ cellHeight - 1
                    data.Cells(row * data.Columns + column).Trigger = eventTable(eventId)
                Case Else
                    Throw New NotImplementedException
            End Select
        Next
    End Sub

    Private Sub ProcessTileLayer(l As TileLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), data As MapData)
        Dim index = 0
        For row = 0 To l.Height - 1
            For column = 0 To l.Width - 1
                Dim cell = l.Data(index)
                Dim tile = tileTable(cell)
                Dim tileProperties = tile.Item1.TileProperties(tile.Item2)
                data.Cells.Add(New MapCellData With
                               {
                                .TerrainType = CType(tileProperties("TerrainType"), TerrainType)
                               })
                index += 1
            Next
        Next
    End Sub
End Module
