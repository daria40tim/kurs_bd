
create table errors( 
e_id serial primary key, 
text varchar(50), 
data_of_err timestamp);

CREATE TABLE sorts(
  sort_id serial primary key,
  s_title varchar(30));


CREATE TABLE stages(
  stage_id serial primary key,
  st_title varchar(30));

CREATE TABLE types(
  type_id serial primary key,
  t_title varchar(30));

CREATE TABLE greenhouses
(
  house_id serial primary key ,
  number integer,
  area integer NOT NULL);
  
create table cultures( 
cult_id serial primary key, 
sort_id integer references sorts(sort_id) on delete set null on update cascade, 
type_id integer references types(type_id) on update cascade on delete set null, 
house_id integer references greenhouses(house_id) on update cascade on delete set null);

create table workers( 
worker_id serial primary key, 
fio varchar(50), 
login varchar(20) unique, 
password varchar(20) unique, 
position varchar(20), 
house_id integer references greenhouses(house_id) on delete set default on update cascade);

create table plants( 
p_id serial primary key, 
house_id integer references greenhouses(house_id) on delete set default on update cascade, 
stage_id integer references stages(stage_id) on delete set default on update cascade, 
cult_id integer, 
count integer);

create table cards( 
card_id serial primary key, 
card_title varchar(50), 
stage_id integer references stages(stage_id) on delete set default on update cascade, 
cult_id integer references cultures(cult_id) on delete set default on update cascade, 
optimal real, 
tolerance real, 
limit_dev real 
);

create table tasks( 
task_id serial primary key, 
task_text text, 
worker_id integer references workers(worker_id) on delete set default on update cascade, 
day_of_app date, 
day_of_ending date, 
result varchar(50) 
);
 

create table crops( 
crop_id serial primary key, 
cult_id integer references cultures(cult_id) on update cascade, 
value integer, 
data_of_crop date 
);
begin; 
insert into stages values 
(default, 'Рассада'), 
(default, 'Рост'), 
(default, 'Цветение'), 
(default, 'Плодоношение') 
returning *; 
end;

begin; 
insert into sorts values 
(default, 'Томат'), 
(default, 'Огурец') 
returning *; 
end;
 
begin; 
insert into types values 
(default, 'Дюшес'), 
(default, 'Розовый мед'), 
(default, 'Sweet cherry'), 
(default, 'Герман F1'), 
(default, 'Артист F1') 
returning *; 
end;

begin; 
insert into greenhouses values 
(default, 1, 2500), 
(default,2,1000), 
(default, 3,1000) 
returning *; 
end;

begin; 
insert into cultures values 
(default, 1,1, 1), 
(default,1,2,1), 
(default, 1,3,1), 
(default, 2,4,2), 
(default, 2,5,3) 
returning *; 
end;

begin; 
insert into plants values 
(default,1,2,1,25), 
(default,1,3,1,50), 
(default,1,4,1,50), 
(default,1,3,2,50), 
(default,1,4,3,50), 
(default,1,4,2,50), 
(default,1,4,3,25) 
returning *; 
end;

begin; 
insert into tasks values 
(default,'Произвести обработку грунта',null, now(), null, null),
(default,'Произвести обработку грунта',null, now(), null, null)  
returning *; 
end;

begin; 
insert into cards values 
(default,'Температура воздуха', 4,2,15,2,15), 
(default,'Влажность почвы', 2,1,90,5,15), 
(default,'Влажность воздуха', 4,3,45,5,15), 
(default,'Освещенность', 3,2,65,5,15) 
returning *; 
end;