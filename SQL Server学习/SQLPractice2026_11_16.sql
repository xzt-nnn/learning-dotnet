CREATE DATABASE SQLPractice;
use SQLPractice;

create table Student(
	id int identity(1,1) primary key, -- 自增逐渐，从1开始，每次+1
	name nvarchar(50) not null,
	age int null,
	gender nvarchar(2),
	createTime DateTime default GETDATE()
);

insert into student(name,age,gender) values (N'张三',18,N'男');

insert into student(name,age,gender) values(N'李四',18,N'男'),(N'王五',18,N'男'),(N'赵六',18,N'男');
insert into student(name,age,gender) values(N'李四',18,N'女'),(N'王五',18,N'女'),(N'赵六',18,'男');
insert into student(name, age, gender) values  (N'张伟',18,N'男'),  (N'王芳',17,N'女'),  (N'李娜',19,N'女'),  (N'刘洋',20,N'男'),  (N'陈明',null,N'男'),  (N'杨静',18,N'女'),  (N'黄强',16,N'男'),  (N'周婷',21,N'女'),  (N'吴浩',19,N'男'),  (N'徐丽',null,N'女'),  (N'赵宇',17,N'男'),  (N'孙悦',20,N'女'),  (N'朱斌',18,N'男'),  (N'高雯',19,N'女'),  (N'林浩',22,N'男'),  (N'何欣',16,N'女'),  (N'罗凯',null,N'男'),  (N'郑瑶',18,N'女'),  (N'谢帆',19,N'男'),  (N'韩雪',20,N'女'),  (N'邓杰',17,N'男'),  (N'曹萌',18,N'女'),  (N'彭程',21,N'男'),  (N'曾悦',null,N'女'),  (N'肖阳',19,N'男'),  (N'田甜',17,N'女'),  (N'董瑞',18,N'男'),  (N'袁琪',20,N'女'),  (N'于洋',16,N'男'),  (N'孟菲',19,N'女'); select * from student;

select id as 编号,name as 姓名,age as 年龄 from student;

-- 查询年龄为18的学生
select * from student where age=18;

-- 18岁并且是女生
select name,gender from student where age=18 and gender=N'女';

-- 年龄17或者19岁
select name as 名字,gender as 性别 from student where age=18 or age=19;

insert into student(name,age,gender) values (N'灭霸',80,'男'),(N'钢铁侠',30,'男');
select * from student;

-- 查询所有男生
select * from student where gender='男';
-- 查询年龄大于等于18的学生
select name,age from student where age >= 18;
-- 查询年龄不是18的男生
select name,age from student where age!=18;

-- 按照年龄从小到大排
select * from student order by age asc;
-- 多字段排序：先按性别，同性别再按年龄降序
select * from student order by gender,age desc;

-- 取前2条学生
select top 2 * from student;
-- 配合排序使用，取年龄最大的2个学生（重点，top一般都搭配order by，不然结果不稳定）
select top 2 * from student order by age desc;

select * from student where name like '王%';
select * from student where name like '_五';

-- 年龄是17、19的学生
select * from student where age in (80,17);

-- 年龄17~19，包含17和19
select * from student where age between 17 and 40;

--1. 查询所有学生，按创建时间倒序
select * from student order by createTime;
--2. 查询名字带 “美” 的学生
select name from student where name like '%四%';
--3. 查询年龄在 18、19 的学生
select * from student where age between 18 and 19;
--4. 查询年龄为空的学生，取前 3 条
select top 3 * from student where age is null;
--5. 查询年龄 16 到 18 之间的学生
select * from student where age between 16 and 18;


-- 总共有多少条学生
select count(*) from student;
-- 有年龄信息的学生数量、平均年龄、最大年龄、最小年龄
select count(*) as 学生数量,avg(age) as 平均年龄,max(age) as 最大年龄,min(age) as 最小年龄 from student where age is not null;

select gender,count(*) as 总数,avg(age) as 平均年龄 from student group by gender;

-- 只保留人数大于等于 15 的性别分组
select gender,count(*) as 总数,avg(age) as 平均年龄 
from student 
group by gender 
having count(*)>=20;

--from → where → group by → 聚合计算 → having → select → order by
-- 先过滤掉 age 为空的学生，再按性别分组，只保留平均年龄大于 18 的分组
select count(*) 数量,gender from student where age is not null group by gender having avg(age)>18;

--写在 select 里的普通字段，
--要么放进 group by，要么包在聚合函数里（max/count/sum 这类），二选一，否则报错。

--1. 按年龄分组，统计每个年龄分别有多少人，只展示人数 >=3 的年龄
select age 年龄,count(*) 总数 from student 
where age is not null group by age having count(*)>=3;
--2. 查询所有年龄大于等于 18 的学生，按性别分组，求出每组最大年龄
select max(age) 最大年龄 from student 
where age is not null and age >=18 group by gender;
--3. 尝试写一条错误 sql：select gender,age,count (*) from student group by gender; 看报错，理解 group by 规则
select gender,age,count (*) from student group by gender;

create table class(
    id int identity(1,1) primary key,
    class_name nvarchar(30) not null,
    teacher nvarchar(20)
);

-- 插入班级数据
insert into class(class_name, teacher)
values
(N'一班', N'张老师'),
(N'二班', N'李老师'),
(N'三班', N'王老师');

-- 给student表新增class_id字段，用来关联班级id
alter table student add class_id int null;

update student set class_id = 1 where id between 1 and 10;
update student set class_id = 2 where id between 11 and 20;
update student set class_id = 3 where id between 21 and 25;
-- id26~30 的学生 class_id 保持 null，代表暂时没有分配班级


--1. `inner join` 内连接：只返回两边匹配上的数据（交集）
--2. `left join` 左连接：左边表全部保留，右边匹配不上的字段填充 null（最常用）
--3. `right join` 右连接：右边表全部保留，左边匹配不上填充 null
--4. `full outer join` 全外连接：两边所有数据都保留，匹配不上的填 null（很少用）

-- 查出有班级的学生
select s.id,s.name,s.age,c.class_name,c.teacher from student s
inner join class c on s.class_id = c.id;

select s.id,s.name,s.age,c.class_name,c.teacher from student s
left join class c on s.class_id = c.id;

select * from student s 
left join class c 
on s.class_id = c.id 
where c.class_name = N'一班';

select * from student s 
left join class c 
on s.class_id = c.id 
and c.class_name = N'一班';

insert into class values (N'四班',N'马牛逼老师');
select s.id,s.name,c.class_name from student s
right join class c on s.class_id=c.id;

select * from student;

--1. left join 查询学生，同时带出班级名称，筛选出一班的学生
select s.name,c.class_name from student s left join class c on c.class_name=N'一班'
--2. 统计每个班级的学生人数（班级名、人数，人数为 0 的班级也要显示出来）
select c.class_name,count(s.id) 总数 from student s 
right join class c on s.class_id =c.id
group by c.class_name;
--3. 找出目前还没有分配班级的学生
select s.name,c.class_name from student s left join class c on s.class_id is null;














