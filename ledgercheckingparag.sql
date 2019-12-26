SELECT    fkvoucherno ,  sum(TVoucherDetails.Debit) as dr, sum(TVoucherDetails.Credit) as cr,TVoucherEntry.ledgerno
FROM         TVoucherEntry INNER JOIN
                      TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo
--where  TVoucherEntry.ledgerno = 3008 --and srno = 501
--where TVoucherEntry.ledgerno in (5,3008, 3928,3917) --and TVoucherEntry.iscancel =0
group by TVoucherEntry.ledgerno ,fkvoucherno
--exec LedgerBookSummary '01-Apr-2017','01-Apr-2030',115,1,'3008'

--Select * from tvoucherentry where ledgerno = 0 
Select * from tvoucherentry  where pkvoucherno = 10142
Select * from tvoucherdetails  where fkvoucherno = 10142

select * from mledger where ledgerno in ( 5, 3928,3917)