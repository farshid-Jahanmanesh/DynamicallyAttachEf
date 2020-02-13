# Dynamically Attach Ef


in this code  Attach Dynamically Entities to Dbset in Your Context and also attach Config to 
OnModelCreating method in your context.

for use it , just need override OnModelCreating method in Context and in the block of code write

modelBuilder.AttachConfig();

for attach Entity Configurations to Your Database for Migration.
and for Dynamically DbSets you need to this code:

modelBuilder.AttachDbSet();

you must Put the class in the same assembly of Entities and config

Entities class must have an attribute in the name of DbSetAttach


for getting data from your Dbset You Can override set method in Your Context class and use it.


One important point is that you must first modelBuilder.AttachConfig(); than after it modelBuilder.AttachDbSet();
