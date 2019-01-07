SELECT * FROM Orders;

INSERT INTO Orders (OrderDate, Client_id, DeliveryType_id, PayType_id)
VALUES ('1955-12-13 12:43:10' , 1, 1, 1);

SELECT o.OrderDate, o.Id, od.OrderDetail_id, p.Name, od.Quantity FROM Orders o join OrderDetails od ON o.Id = od.Order_id join Products p on od.Product_id = p.Id WHERE o.OrderDate >= convert(datetime,'2019-01-01');

select * from Payments;

select sum(Total_value) - sum(Pay_value)  as Total_Sum  from Payments;

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