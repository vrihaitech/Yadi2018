SELECT     MItemGroup.ItemGroupName, MItemMaster.ItemName, MItemMaster.ItemNo, SUM(TStock.Quantity) AS sqty,  MUOM.UOMName,SUM(TStock.Amount) AS amount
FROM         TStock INNER JOIN
                      MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN
                      TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
                      MUOM ON TStock.FkUomNo = MUOM.UOMNo
WHERE     (TVoucherEntry.VoucherTypeCode = 9) and TVoucherEntry.iscancel = 0
GROUP BY MItemGroup.ItemGroupName, MItemMaster.ItemName, MItemMaster.ItemNo, MUOM.UOMName
ORDER BY MItemGroup.ItemGroupName, MItemMaster.ItemName, MItemMaster.ItemNo, MUOM.UOMName