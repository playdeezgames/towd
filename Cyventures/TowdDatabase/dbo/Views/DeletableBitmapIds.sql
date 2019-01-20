CREATE VIEW [dbo].[DeletableBitmapIds]
	AS
SELECT
	b.BitmapSequenceId,
	MAX(b.BitmapIndex) DeletableBitmapId
FROM
	Bitmaps b
GROUP BY
	b.BitmapSequenceId
