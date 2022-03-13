.Net6 Kafka Template

Extends exception Filters and OneOF

1. add new event to Core Events.
2. Cycle detected error. When two projects have same name.
   I.e. "Core". Even if they are in different folders.
   Just rename one of them. Otherwise the solution is not stable.

3. In project Kafka specify Dependencies, which include connections to Kafka.(There is options pattern.)
4. Create PayloadSentPublisher.
5. Create PayloadSent and PayloadSentEvent (with Avro Schema).
6. Setup Kafka emulator in docker from docker-compose.yml (docker-compose up).
7. Fill appsettings.json with values for Kafka. (Check values with docker-compose.yml file)
8. Localhost:9021 - control center. It is Kafka UI.
9. Add new topic. Check name in appsettings.json
10. Go to Schemas. Set a schema. Avro type. Take schema from PayloadSentEvent class. Delete all '\' symbols before pasting schema in Confluent UI.
11. Set only value. Key may be empty.
12. You may receive error. "Schema registry is not set up". Make sure that Schema registry is up.
    Then verify that the SCHEMA_REGISTRY_LISTENERS (http://0.0.0.0:8083) configuration matches the Control Center CONTROL_CENTER_SCHEMA_REGISTRY_URL ("http://schema-registry:8083") configuration. Port should be same! But domain can be different.

    For details read: https://docs.confluent.io/platform/current/kafka/authentication_sasl/authentication_sasl_plain.html#kafka-sasl-auth-plain

    Configuring Authorization is inaccessible on free emulator. Only on license!

    So, either remove username/password check, or buy license.

    !!! Kafka isn`t receivimg messages! Avro serialization error. Maybe reset to version v=when we used emulator and then improve it.

    Im docker folder there are scripts how to congigure kafka. I didn`t try to run them, but they might be useful!

13. Watch all the events published to kafka emulator

In docker folder lay docker compose files for cosmos and kafka.
Julie - is terraform for kafka.

Notice! I Created OutgoingShared as I cannot referense Shared.Common from Kafka project.
There is an issue with circular references. So I can only create a new Shared project and reference it from Kafka.
