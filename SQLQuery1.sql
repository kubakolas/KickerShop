

-- Procedura do query2 podająca satystykę związaną z zaoszczędzonymi pieniędzmi ze znizki
go
CREATE   procedure DiscountValue
as
begin
 declare @total_value float
 declare @total_disconted_value float
 declare @distinction float
 declare @dicont_pro float

 select @total_value =  sum(Total_value) from Payments;
 select @total_disconted_value =  sum(Pay_value) from Payments;
 select @distinction = @total_value - @total_disconted_value;
 select @dicont_pro = (@distinction / @total_disconted_value) *100;

 select @total_value as Total_value, @total_disconted_value as Total_Disconted_Value, @distinction as  Distinction, @dicont_pro as Procent;
end

drop procedure DiscountValue

--procedura związana z query3 podająca n najbardziej dochodowych produktow

go
CREATE procedure MostProfitableProducts
@number int
as
begin
	select top (@number) od.Product_id, p.Name, sum(od.Quantity * p.Unit_price) as Total 
	from OrderDetails od join Products p on od.Product_id = p.Id 
	group by od.Product_id, p.Name
	order by Total DESC;  
end

drop procedure MostProfitableProducts;

select * from Payments;

Select count(p.Payment_id) as FreeDelivery from Payments p where p.Delivery_cost = 0;


-- widok pozwalajacy na generowania raportu
go
create view Report as
select ISNULL(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)), -999)
as Id, year(o.OrderDate) 
as Jare, sum(od.Quantity * p.Unit_price) 
as Total, count(o.Id) 
as Order_Count, sum(od.Quantity * p.Unit_price) / count(o.Id) 
as Avg_Per_Order  
from Orders o 
join OrderDetails od on o.Id = od.Order_id 
join Products p on p.Id = od.Product_id
group by year(o.OrderDate);

drop view Report;

select * from Report;
