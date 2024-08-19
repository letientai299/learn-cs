drop table if exists messages;

create table messages
(
    id         text primary key,
    created_at timestamp without time zone
);

insert into messages(id, created_at) values ('hello world', now());
